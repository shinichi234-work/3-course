n = int(input())
arr = list(map(int, input().split()))

arr.sort(key=lambda x: (x % 2, x))

print(*arr)