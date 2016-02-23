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
    public class FinboardCollection : IncrementalLoadingBase<Finboard>
    {
        private readonly IFindierService _findierService;
        private readonly GetFinboardsRequest _request;
        private FindierResponse<FindierPageData<Finboard>> _currentResponse;

        public FinboardCollection(GetFinboardsRequest request, IFindierService findierService)
        {
            _request = request;
            _findierService = findierService;
        }

        internal FinboardCollection(Surrogate surrogate, IFindierService findierService)
        {
            foreach (var items in surrogate.Items)
            {
                Add(items);
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

        protected override async Task<IList<Finboard>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            var response = await _findierService
                .SendAsync<GetFinboardsRequest, FindierPageData<Finboard>>(_request.Offset(Count).Limit((int)count));
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

            internal Surrogate(FinboardCollection collection)
            {
                Items = collection.ToList();
                CurrentResponse = collection._currentResponse;
                Request = collection._request;
            }

            public FindierResponse<FindierPageData<Finboard>> CurrentResponse { get; set; }

            public List<Finboard> Items { get; set; }

            public GetFinboardsRequest Request { get; set; }

            public FinboardCollection ToCollection(IFindierService apiService)
            {
                return new FinboardCollection(this, apiService);
            }
        }
    }
}