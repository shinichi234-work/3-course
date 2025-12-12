n, k = map(int, input().split())
cards = list(map(int, input().split()))
cards_sorted = sorted(cards)

from collections import Counter
card_count = Counter(cards)

variants = set()
left = 0

for right in range(len(cards_sorted)):
    while cards_sorted[right] > k * cards_sorted[left]:
        left += 1
    
    window = cards_sorted[left:right + 1]
    
    for i in range(len(window)):
        for j in range(len(window)):
            for l in range(len(window)):
                a, b, c = window[i], window[j], window[l]
                
                if a == b == c:
                    if card_count[a] >= 3:
                        variants.add((a, b, c))
                elif a == b or b == c or a == c:
                    equal_val = a if a == b else (b if b == c else a)
                    other_val = c if a == b else (a if b == c else b)
                    if card_count[equal_val] >= 2:
                        variants.add((a, b, c))
                else:
                    variants.add((a, b, c))

print(len(variants))