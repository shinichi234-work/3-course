def generate_strings(n, k, current=""):
    if len(current) == n:
        print(current)
        return
    
    for i in range(k - 1, -1, -1):
        if i < 10:
            char = str(i)
        else:
            char = chr(ord('a') + i - 10)
        generate_strings(n, k, current + char)

n, k = map(int, input().split())
generate_strings(n, k)