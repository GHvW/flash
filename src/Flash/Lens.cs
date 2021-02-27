using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash {

    public record Lens<A, B>(Func<A, B> Get, Func<B, A, A> Set);
}
