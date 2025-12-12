def generate_binary(n, current=""):
    if len(current) == n:
        print(current)
        return
    
    generate_binary(n, current + "0")
    generate_binary(n, current + "1")

n = int(input())
generate_binary(n)