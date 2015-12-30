using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCoder
{
    class Match<A, T>
    {
        private A _argument;
        private bool _match;
        private T _result;

        private Match(A argument, bool match, T result)
        {
            _argument = argument;
            _match = match;
            _result = result;
        }

        public static Match<A, T> NoResult(A argument)
        {
            return new Match<A, T>(argument, false, default(T));
        }

        public static Match<A, T> Result(A argument, T result)
        {
            return new Match<A, T>(argument, true, result);
        }

        public Match<A, T> Or(Func<A, Match<A, T>> next)
        {
            if (_match)
                return this;
            else
                return next(_argument);
        }

        public Match<A, T> OnMatch(Action<T> action)
        {
            if (_match)
                action(_result);
            return this;
        }
    }
}
