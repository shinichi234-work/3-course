def generate_partitions(n, max_val=None, current=[]):
    if max_val is None:
        max_val = n
    
    if n == 0:
        print(*current)
        return
    
    for i in range(1, min(n, max_val) + 1):
        generate_partitions(n - i, i, current + [i])

n = int(input())
generate_partitions(n)