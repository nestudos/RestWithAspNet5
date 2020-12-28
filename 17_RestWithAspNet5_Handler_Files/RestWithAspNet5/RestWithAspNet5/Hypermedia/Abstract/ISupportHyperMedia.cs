using System.Collections.Generic;

namespace RestWithAspNet5.Hypermedia.Abstract
{
    public interface ISupportHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
