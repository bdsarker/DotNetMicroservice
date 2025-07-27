using WebApp.Models;
using WebApp.Models.Dto;

namespace WebApp.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
