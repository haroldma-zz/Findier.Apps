﻿using System.IO;
using System.Threading.Tasks;
using Findier.Core.Helpers;

namespace Findier.Core.Utilities.Interfaces
{
    public interface IStorageUtility
    {
        Task<bool> DeleteAsync(
            string path,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task<bool> ExistsAsync(
            string path,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task<byte[]> ReadBytesAsync(
            string path,
            bool ifExists = false,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task<Stream> ReadStreamAsync(
            string path,
            bool ifExists = false,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task<string> ReadStringAsync(
            string path,
            bool ifExists = false,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task WriteBytesAsync(
            string path,
            byte[] bytes,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task WriteStreamAsync(
            string path,
            Stream stream,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);

        Task WriteStringAsync(
            string path,
            string text,
            PclStorageHelper.StorageStrategy location = PclStorageHelper.StorageStrategy.Local);
    }
}