using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Map<K, V>
{
    private List<HashNode<K, V>> bucketArr;
    private int bucketNum;
    private int bucketSize;


    public Map()
    {
        bucketArr = new List<HashNode<K, V>>();
        bucketNum = 10;
        bucketSize = 0;
        for (int i = 0; i < bucketNum; i++)
        {
            bucketArr.Add(null);
        }
    }
    public Map(int n)
    {
        bucketArr = new List<HashNode<K, V>>();
        bucketNum = n;
        bucketSize = 0;

        for (int i = 0; i < bucketNum; i++)
            bucketArr.Add(null);
    }
    public int Size { get { return Size; } }
    public bool isEmpty() { return Size == 0; }

    private int HashCode(K key)
    {
        return key.GetHashCode();
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = HashCode(key);
        int i = hashCode % bucketNum;
        i = i < 0 ? i * -1 : i;
        return i;
    }
    public V Remove(K key)
    {
        int bucketInd = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArr[bucketInd];

        HashNode<K, V> previous = null;
        while (head != null)
        {
            if (head.key.Equals(key) && hashCode == head.HashCode)
                break;

            previous = head;
            head = head.next;
        }
        if (head == null)
            return default(V);
        bucketSize--;
        if (previous != null)
            previous.next = head.next;
        else
            bucketArr[bucketInd] = head.next;
        return head.value;
    }
    public V Get(K key)
    {
        int bucketInd = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArr[bucketInd];
        while(head != null)
        {
            if (head.key.Equals(key) && head.HashCode == hashCode)
                return head.value;
            head = head.next;
        }
        return default(V);
    }

    public void add(K key, V value)
    {
        int bucketInd = GetBucketIndex(key);
        int hashCode = HashCode(key);
        HashNode<K, V> head = bucketArr[bucketInd];
        while (head != null)
        {
            if (head.key.Equals(key) && head.HashCode == hashCode)
            {
                head.value = value;
                return;
            }
            head = head.next;
        }
        bucketSize++;
        head = bucketArr[bucketInd];
        HashNode<K, V> NewNode = new HashNode<K, V>(key, value, hashCode);
        NewNode.next = head;
        bucketArr[bucketInd] = NewNode;

        if ((1.0 * bucketSize) / bucketNum >= 0.7)
        {
            List<HashNode<K, V>> tmp = bucketArr;
            bucketArr = new List<HashNode<K, V>>();
            bucketNum = 2 * bucketNum;
            bucketSize = 0;
            for (int i = 0; i < bucketNum; i++)
                bucketArr.Add(null);
            foreach (HashNode<K, V> headNode in tmp)
            {
                HashNode<K, V> currentNode = headNode;
                while (currentNode != null)
                {
                    add(currentNode.key, currentNode.value);
                    currentNode = currentNode.next;
                }
            }
        }
    }
        
    public override string  ToString()
    {
        string str = "";
        for (int i = 0; i < bucketNum; i++)
        {
            str += $"{i}: ";
            HashNode<K, V> node = bucketArr[i];
            while (node != null)
            {
                str += $"{node.value} ";
                node = node.next;
            }
            str += "\n";
        }
        return str;
    }
}



     


















