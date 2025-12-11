pos = input()
col = ord(pos[0]) - ord('a')
row = 8 - int(pos[1])

board = [['.' for _ in range(8)] for _ in range(8)]
board[row][col] = 'K'

moves = [(-2, -1), (-2, 1), (-1, -2), (-1, 2), (1, -2), (1, 2), (2, -1), (2, 1)]

for dr, dc in moves:
    new_row = row + dr
    new_col = col + dc
    if 0 <= new_row < 8 and 0 <= new_col < 8:
        board[new_row][new_col] = '*'

for row in board:
    print(' '.join(row))