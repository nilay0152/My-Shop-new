using SMS.Data.Database;
using System;

namespace SMS.Data
{
    public class BaseProvider : IDisposable
    {

        public StudentEntites _db;
        public BaseProvider()
        {
            _db = new StudentEntites();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
