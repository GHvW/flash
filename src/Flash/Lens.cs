using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flash {

    public record Lens<A, B>(Func<A, B> Get, Func<B, A, A> Set) {

        public Lens<A, C> Compose<C>(Lens<B, C> other) =>
            new Lens<A, C>(
                Get: (a) => other.Get(this.Get(a)),
                Set: (c, a) => this.Set(other.Set(c, this.Get(a)), a));
    }
}
