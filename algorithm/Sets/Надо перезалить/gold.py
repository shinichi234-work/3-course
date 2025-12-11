n = int(input())
numbers = list(map(int, input().split()))

current = []
result = []

for num in numbers:
    left, right = 0, len(current)
    while left < right:
        mid = (left + right) // 2
        if current[mid] < num:
            left = mid + 1
        else:
            right = mid
    current.insert(left, num)
    
    median_index = (len(current) + 1) // 2 - 1
    result.append(current[median_index])

print(*result)