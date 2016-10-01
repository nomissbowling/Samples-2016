﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TaskWebJob
{
    public static class Functions
    {
        [NoAutomaticTrigger]
        public static void RecordTimeTest(
            DateTime startTime,
            IBinder binder,
            TextWriter logger)
        {
            var start = $"{startTime:yyyyMMdd-HHmmss}";

            var blobAttribute = new BlobAttribute($"output/{start}");
            var blob = binder.Bind<CloudBlockBlob>(blobAttribute);

            blob.UploadText(start);
            logger.WriteLine(start);

            //RecordTime(start, blob, logger);
            //RecordTimeAndSleep(start, blob, logger);
            RecordTimes(start, blob, logger);
        }

        public static void RecordTime(string startString, CloudBlockBlob blob, TextWriter logger)
        {
        }

        public static void RecordTimeAndSleep(string startString, CloudBlockBlob blob, TextWriter logger)
        {
            Thread.Sleep(10 * 60 * 1000);
        }

        public static void RecordTimes(string startString, CloudBlockBlob blob, TextWriter logger)
        {
            while (true)
            {
                Thread.Sleep(20 * 1000);

                var now = $"{DateTime.UtcNow:yyyyMMdd-HHmmss}";
                blob.UploadText($"{startString}\r\n{now}");

                logger.WriteLine(now);
            }
        }
    }
}
