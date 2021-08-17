using System;
using System.Threading.Tasks;
using FakeNewsFilter.ViewModel.Catalog.Media;

namespace FakeNewsFilter.Application.Catalog.MediaManager
{
    public interface IManageMediaService
    {
        Task<int> RemoveMedia(int mediaId);

        Task<int> UpdateMedia(int mediaId, NewsMediaCreateRequest request);
    }
}