n = int(input())
arr = list(map(int, input().split()))
value, position = map(int, input().split())

arr.insert(position - 1, value)

print(*arr)