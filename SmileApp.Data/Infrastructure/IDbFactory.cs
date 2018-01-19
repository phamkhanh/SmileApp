using System;

namespace SmileApp.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        SmileAppDbContext Init();
    }
}