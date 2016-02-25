using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Responses;
using Findier.Web.Services;
using Findier.Windows.Common;

namespace Findier.Windows.IncrementalLoading
{
    public class PostsCollection : IncrementalLoadingBase<Post>
    {
        private readonly IFindierService _findierService;
        private readonly GetPostsRequest _request;
        private FindierResponse<FindierPageData<Post>> _currentResponse;

        public PostsCollection(GetPostsRequest request, IFindierService findierService)
        {
            _request = request;
            _findierService = findierService;
        }

        internal PostsCollection(Surrogate surrogate, IFindierService findierService)
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

        protected override async Task<IList<Post>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var response = await _findierService
                .SendAsync<GetPostsRequest, FindierPageData<Post>>(_request.Offset(Count).Limit((int)count));
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

            internal Surrogate(PostsCollection collection)
            {
                Items = collection.ToList();
                CurrentResponse = collection._currentResponse;
                Request = collection._request;
            }

            public FindierResponse<FindierPageData<Post>> CurrentResponse { get; set; }

            public List<Post> Items { get; set; }

            public GetPostsRequest Request { get; set; }

            public PostsCollection ToCollection(IFindierService apiService)
            {
                return new PostsCollection(this, apiService);
            }
        }
    }
}