def min_drops(n):
    if n <= 1:
        return n
    if n == 2:
        return 1
    
    min_result = n
    
    for k in range(1, n + 1):
        worst = 1 + max(k - 1, min_drops(n - k))
        min_result = min(min_result, worst)
    
    return min_result

n = int(input())
print(min_drops(n))