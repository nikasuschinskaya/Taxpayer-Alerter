using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxpayerAlerter.DAL.ReadWorkers;
using TaxpayerAlerter.DAL.ReadWorkers.Base;
using TaxpayerAlerter.DAL.WriteWorkers;

namespace TaxpayerAlerter.BLL.Workers
{
    public class Worker
    {
        private readonly XLSXReadWorker _xlsxReadWorker;
        private readonly DOCXWriteWorker _docWriteWorker;
        private readonly XLSXWriteWorker _xlsxWriteWorker;
        private readonly ILogger<Worker> _logger;

        public Worker(XLSXReadWorker xlsxReadWorker,
                      DOCXWriteWorker docWriteWorker,
                      XLSXWriteWorker xlsxWriteWorker,
                      ILogger<Worker> logger)
        {
            _xlsxReadWorker = xlsxReadWorker;
            _docWriteWorker = docWriteWorker;
            _xlsxWriteWorker = xlsxWriteWorker;
            _logger = logger;
        }

        public async Task StartWorkAsync()
        {
            //проходит дата либо выбранная, либо раньше
        }
    }
}
