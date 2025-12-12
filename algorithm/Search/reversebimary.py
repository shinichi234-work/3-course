def generate_binary(n, current=""):
    if len(current) == n:
        print(current)
        return
    
    generate_binary(n, current + "1")
    generate_binary(n, current + "0")

n = int(input())
generate_binary(n)