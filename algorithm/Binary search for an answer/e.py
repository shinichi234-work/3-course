n, x, y = map(int, input().split())

if n == 1:
    print(min(x, y))
else:
    first_copy_time = min(x, y)
    n -= 1
    
    left = 0
    right = n * max(x, y)
    result = right
    
    while left <= right:
        mid = (left + right) // 2
        
        copies = mid // x + mid // y
        
        if copies >= n:
            result = mid
            right = mid - 1
        else:
            left = mid + 1
    
    print(result + first_copy_time)