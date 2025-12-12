def generate_sequences(n, k):
    def backtrack(current, start):
        if len(current) == k:
            print(*current)
            return
        
        for i in range(start, n + 1):
            if n - i + 1 >= k - len(current):
                current.append(i)
                backtrack(current, i + 1)
                current.pop()
    
    backtrack([], 1)

n, k = map(int, input().split())
generate_sequences(n, k)