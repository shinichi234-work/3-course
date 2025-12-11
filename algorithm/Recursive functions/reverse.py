def reverse_sequence():
    n = int(input())
    if n == 0:
        print(0)
        return
    reverse_sequence()
    print(n)

reverse_sequence()