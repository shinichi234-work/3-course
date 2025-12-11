def can_reach(n, memo={}):
    if n in memo:
        return memo[n]
    
    if n == 1:
        return True
    if n < 1:
        return False
    
    result = can_reach(n - 3, memo) or can_reach(n - 5, memo)
    memo[n] = result
    return result

n = int(input())
if can_reach(n):
    print("YES")
else:
    print("NO")