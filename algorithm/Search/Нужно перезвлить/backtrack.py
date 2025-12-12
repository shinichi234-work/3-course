def generate_sequences(n, k):
    def backtrack(current):
        if len(current) == k:
            print(*current)
            return
        
        start = current[-1] - 1 if current else n
        
        for i in range(start, 0, -1):
            current.append(i)
            backtrack(current)
            current.pop()
    
    backtrack([])

n, k = map(int, input().split())
generate_sequences(n, k)