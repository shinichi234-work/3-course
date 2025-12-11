import sys
sys.setrecursionlimit(500000)

def power(a, n):
    if n == 0:
        return 1
    return a * power(a, n - 1)

a = int(input())
n = int(input())
print(power(a, n))