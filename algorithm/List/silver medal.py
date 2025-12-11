n = int(input())
results = list(map(int, input().split()))

max1 = max(results)

max2 = -float('inf')
for score in results:
    if score < max1 and score > max2:
        max2 = score

print(max2)