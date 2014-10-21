using PvcCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvcPlugins
{
    public class PvcCommonMark : PvcPlugin
    {
        public override string[] SupportedTags
        {
            get
            {
                return new string[] { ".md" };
            }
        }

        public override IEnumerable<PvcStream> Execute(IEnumerable<PvcStream> inputStreams)
        {
            var outputStreams = new ConcurrentBag<PvcStream>();
            Parallel.ForEach<PvcStream>(inputStreams, async inputStream =>
            {
                using (var reader = new StreamReader(inputStream))
                {
                    var ms = new MemoryStream();
                    var writer = new StreamWriter(ms);

                    CommonMark.CommonMarkConverter.Convert(reader, writer);
                    await writer.FlushAsync();

                    var outputStream = new PvcStream(() => ms)
                        .As(inputStream.StreamName, inputStream.OriginalSourcePath);

                    outputStreams.Add(outputStream);
                }
            });
            return outputStreams;
        }
    }
}
