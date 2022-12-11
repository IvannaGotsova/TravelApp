using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Services;
using TravelApp.Data;

namespace TravelApp.Tests.Mocks
{
    public static class DatabaseMock
    {
        
        public static TravelAppDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<TravelAppDbContext>()                         
                    .UseInMemoryDatabase("TravelAppInMemoryDb" 
                    + DateTime.Now.Ticks.ToString())
                    .Options;

                return new TravelAppDbContext(dbContextOptions, false);
            }
        }
    }
}
