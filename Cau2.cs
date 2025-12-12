class Program
{
    const long MOD = 1000000007;

    static long Binpow(long a, long b)
    {
        if (b == 0) return 1;

        long X = Binpow(a, b / 2);
        if (b % 2 == 1)
        {
            return X * X * a % MOD;
        }
        else
        {
            return X * X % MOD;
        }
    }
    static void Main()
    {
        long n = long.Parse(Console.ReadLine()!);
        long odd = (n + 1) / 2;
        long even = n / 2;

        long result = Binpow(5, odd) * Binpow(4, even) % MOD;
        Console.WriteLine(result);
    }
}

