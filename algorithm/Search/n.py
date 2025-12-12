def count_queens(n):
    def is_safe(board, row, col):
        for i in range(row):
            if board[i] == col:
                return False
            if abs(board[i] - col) == abs(i - row):
                return False
        return True
    
    def solve(board, row):
        if row == n:
            return 1
        
        count = 0
        for col in range(n):
            if is_safe(board, row, col):
                board[row] = col
                count += solve(board, row + 1)
                board[row] = -1
        
        return count
    
    return solve([-1] * n, 0)

n = int(input())
print(count_queens(n))