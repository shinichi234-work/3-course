n = int(input())
prices = list(map(int, input().split()))

prices.sort(reverse=True)

total = 0
for i in range(n):
    if (i + 1) % 3 != 0:
        total += prices[i]

print(total)