using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<AppContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;

            // to Debug seed method.

            // if (System.Diagnostics.Debugger.IsAttached == false)

            // {

            // System.Diagnostics.Debugger.Launch();

            // }
        }
        protected override void Seed(AppContext context)
        {

        }
    }
}
