using System;

namespace ASiNet.Data.Serialization.Hash
{
    public class PolynomialHasher
    {

        public static PolynomialHasher Shared => _shared.Value;

        private static Lazy<PolynomialHasher> _shared = new Lazy<PolynomialHasher>(() => new PolynomialHasher());

        private const int _defaultAsciiOffset = 32;
        private const int _alphabet = 137;
        private const long _module1 = 1000000007;
        private const long _module2 = 1000019711;

        private long[] _precalc1 = new long[64];
        private long[] _precalc2 = new long[64];
        private int _calculatedCount = 0;

        public long CalculateHash(string s)
        {
            if (_calculatedCount < s.Length)
                PreCalculateFor(s.Length);

            long hash1 = 1;
            long hash2 = 1;
            for (int i = 0; i < s.Length; i++)
            {
                int numChar = (s[i] - _defaultAsciiOffset + 1);
                hash1 += _precalc1[i] * numChar;
                hash1 %= _module1;

                hash2 += _precalc2[i] * numChar;
                hash2 %= _module2;
            }

            return ((hash2 << 32) | hash1);
        }

        private void PreCalculateFor(int n)
        {
            while (_precalc1.Length < n)
            {
                Array.Resize(ref _precalc1, _precalc1.Length << 2);
                Array.Resize(ref _precalc2, _precalc2.Length << 2);
            }

            if (_calculatedCount == 0)
            {
                _calculatedCount = 1;

                _precalc1[0] = 1;
                _precalc2[0] = 1;
            }

            for (; _calculatedCount < n; _calculatedCount++)
            {
                _precalc1[_calculatedCount] = _precalc1[_calculatedCount - 1] * _alphabet;
                _precalc1[_calculatedCount] %= _module1;

                _precalc2[_calculatedCount] = _precalc2[_calculatedCount - 1] * _alphabet;
                _precalc2[_calculatedCount] %= _module2;
            }
        }

        public static long CalculateHashLowMem(string s)
        {
            long b1 = 1;
            long b2 = 1;
            long hash1 = 1;
            long hash2 = 1;
            for (int i = 0; i < s.Length; i++)
            {
                int numChar = (s[i] - _defaultAsciiOffset + 1);
                hash1 += b1 * numChar;
                hash1 %= _module1;

                hash2 += b2 * numChar;
                hash2 %= _module2;

                b1 *= _alphabet;
                b1 %= _module1;

                b2 *= _alphabet;
                b2 %= _module2;
            }

            return ((hash2 << 32) | hash1);
        }
    }
}