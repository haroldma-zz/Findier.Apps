using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Findier.Client.Windows.Common;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Responses;
using Findier.Web.Services;

namespace Findier.Client.Windows.IncrementalLoading
{
    public class CommentCollection : IncrementalLoadingBase<Comment>
    {
        private readonly IFindierService _findierService;
        private readonly GetPostCommentsRequest _request;
        private FindierBaseResponse<FindierPageData<Comment>> _currentResponse;

        public CommentCollection(GetPostCommentsRequest request, IFindierService findierService)
        {
            _request = request;
            _findierService = findierService;
        }

        internal CommentCollection(Surrogate surrogate, IFindierService findierService)
        {
            foreach (var item in surrogate.Items)
            {
                Add(item);
            }
            _request = surrogate.Request;
            _findierService = findierService;
            _currentResponse = surrogate.CurrentResponse;
        }

        public Surrogate ToSurrogate()
        {
            return new Surrogate(this);
        }

        protected override bool HasMoreItemsOverride()
        {
            return _currentResponse?.Data?.HasNext ?? true;
        }

        protected override async Task<IList<Comment>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var response = await _findierService
                .SendAsync<GetPostCommentsRequest, FindierPageData<Comment>>(_request.Offset(Count).Limit((int)count));
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            _currentResponse = response.DeserializedResponse;

            return _currentResponse.Data.Results;
        }

        // and contains the same data as CommentIncrementalLoadingCollection
        /// <summary>
        ///     Intermediate class that can be serialized by JSON.net
        /// </summary>
        public class Surrogate
        {
            public Surrogate()
            {
            }

            internal Surrogate(CommentCollection collection)
            {
                Items = collection.ToList();
                CurrentResponse = collection._currentResponse;
                Request = collection._request;
            }

            public FindierBaseResponse<FindierPageData<Comment>> CurrentResponse { get; set; }

            public List<Comment> Items { get; set; }

            public GetPostCommentsRequest Request { get; set; }

            public CommentCollection ToCollection(IFindierService apiService)
            {
                return new CommentCollection(this, apiService);
            }
        }
    }
}