def generate_partitions(n, min_val=1, current=[]):
    if n == 0:
        print(*current)
        return
    
    for i in range(min_val, n + 1):
        generate_partitions(n - i, i, current + [i])

n = int(input())
generate_partitions(n)