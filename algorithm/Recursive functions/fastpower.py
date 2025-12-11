def fast_power(a, n):
    if n == 0:
        return 1
    if n % 2 == 0:
        half = fast_power(a, n // 2)
        return half * half
    else:
        return a * fast_power(a, n - 1)

a = float(input())
n = int(input())
print(fast_power(a, n))