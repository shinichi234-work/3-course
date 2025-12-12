n, r = map(int, input().split())
d = list(map(int, input().split()))

count = 0
right = 0

for left in range(n):
    while right < n and d[right] - d[left] <= r:
        right += 1
    
    count += n - right

print(count)