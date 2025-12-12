def generate_strings(n, k, current="", ones=0):
    if len(current) == n:
        if ones == k:
            print(current)
        return
    
    remaining = n - len(current)
    
    if ones < k:
        generate_strings(n, k, current + "1", ones + 1)
    
    if ones + remaining > k:
        generate_strings(n, k, current + "0", ones)

n, k = map(int, input().split())
generate_strings(n, k)