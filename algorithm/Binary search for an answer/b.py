n = int(input())
k = int(input())

left = 1
right = 2000000000
result = right

while left <= right:
    mid = (left + right) // 2
    
    total = mid * (2 * k + mid - 1) // 2
    
    if total >= n:
        result = mid
        right = mid - 1
    else:
        left = mid + 1

print(result)