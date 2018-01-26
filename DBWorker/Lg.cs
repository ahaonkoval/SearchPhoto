using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DBWorker
{
    public class Lg
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Lg()
        {

        }

        public void Log(string message) {
            _logger.Info(message);
        }
    }
}
