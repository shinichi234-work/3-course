def generate_partitions(n, min_val=1, current=[]):
    if n == 0:
        print(*current)
        return
    
    for i in range(n, min_val - 1, -1):
        generate_partitions(n - i, max(i, min_val), current + [i])

n = int(input())
generate_partitions(n)