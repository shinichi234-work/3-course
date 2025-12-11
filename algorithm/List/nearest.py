n = int(input())
arr = list(map(int, input().split()))
x = int(input())
closest = arr[0]
min_diff = abs(arr[0] - x)
for num in arr:
    diff = abs(num - x)
    if diff < min_diff:
        min_diff = diff
        closest = num
print(closest)