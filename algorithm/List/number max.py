n = int(input())
arr = list(map(int, input().split()))
max_value = max(arr)
print(arr.index(max_value) + 1)