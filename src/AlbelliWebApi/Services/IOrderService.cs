using AlbelliWebApi.Models.Requests;
using AlbelliWebApi.Models.Responses;
namespace AlbelliWebApi.Services
{
    public interface IOrderService
    {
        BaseResponse<string> Create(OrderRequest request);
        BaseResponse<OrderResponse> Get(string orderId);
    }
}
