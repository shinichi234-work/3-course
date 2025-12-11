def fill_cells(n, result):
    if n == 1:
        result.append(1)
        return
    
    fill_cells(n - 1, result)
    result.append(n)
    result.append(-1)
    fill_cells(n - 1, result)
    result.append(1)

n = int(input())
result = []
fill_cells(n, result)
print(*result)