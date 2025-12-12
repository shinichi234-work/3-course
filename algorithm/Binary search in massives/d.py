n = int(input())
first = list(map(int, input().split()))
m = int(input())
second = list(map(int, input().split()))

count = {}
for num in first:
    count[num] = count.get(num, 0) + 1

result = []
for num in second:
    result.append(str(count.get(num, 0)))

print(' '.join(result))