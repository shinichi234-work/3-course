def generate_strings(n, k, current=""):
    if len(current) == n:
        print(current)
        return
    
    for i in range(k):
        generate_strings(n, k, current + str(i))

n, k = map(int, input().split())
generate_strings(n, k)