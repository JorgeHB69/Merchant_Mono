import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import ShoppingCartItem from "@/commons/entities/ShoppingCartItem";
import { handleSubmitCart } from "@/services/checkoutService";
import { CartData } from "@/schemes/shopping-cart/CartDataDto";
import useAuth from "@/commons/hooks/useAuth";
import { useRouter } from "next/navigation";
import { toast } from "sonner";

interface Types {
  products: Array<ShoppingCartItem>;
  addProductToCart: (newProduct: ShoppingCartItem | null) => void;
  initializeShoppingCart: () => void;
  deleteProductToCart: (id: string) => void;
  increaseQuantityProduct: (id: string) => void;
  decreaseQuantityProduct: (id: string) => void;
  changeQuantity: (id: string, quantity: number) => void;
  handleStripe: () => void;
  clearShoppingCart: () => void;
}

interface Props {
  children: ReactNode;
}

const ShoppingCartContext = createContext<Types | undefined>(undefined);

export const ShoppingCartProvider = ({ children }: Props) => {
  const [products, setProducts] = useState<Array<ShoppingCartItem>>([]);
  const { user } = useAuth();
  const router = useRouter();

  const addProductToCart = (newProduct: ShoppingCartItem | null) => {
    if (newProduct) {
      setProducts((prevProducts) => {
        const existingProduct = prevProducts.find(
          (product) => product.id === newProduct.id
        );
        if (existingProduct) {
          toast.info('This product has already been added', {
            id: `info-${newProduct.id}`,
            duration: 3000
          });
          return prevProducts;
        } else {
          toast.success('Product added to cart', {
            id: `success-${newProduct.id}`,
            duration: 3000
          });
          return [...prevProducts, newProduct];
        }
      });
    }
  };

  const deleteProductToCart = (id: string) => {
    const updatedProducts = products.filter((product) => product.id !== id);
    setProducts(updatedProducts);
    toast.error('Product deleted');
  };

  const updateLocalStorage = () => {
    localStorage.setItem("shoppingCartItems", JSON.stringify(products));
  };

  const initializeShoppingCart = () => {
    const existingCartItems = JSON.parse(
      localStorage.getItem("shoppingCartItems") ?? "[]"
    );

    setProducts(existingCartItems);
  };

  const increaseQuantityProduct = (id: string) => {
    setProducts((prevProducts) =>
      prevProducts.map((product) =>
        product.id === id
          ? { ...product, quantity: product.quantity + 1 }
          : product
      )
    );
  };

  const decreaseQuantityProduct = (id: string) => {
    setProducts((prevProducts) =>
      prevProducts.map((product) =>
        product.id === id && product.quantity > 1
          ? { ...product, quantity: product.quantity - 1 }
          : product
      )
    );
  };

  const changeQuantity = (id: string, quantity: number) => {
    if (quantity < 1) return;
    setProducts((prevProducts) =>
      prevProducts.map((product) =>
        product.id === id ? { ...product, quantity } : product
      )
    );
  };

  const handleStripe = () => {
    if (!user) {
      router.push("/login");
      toast.error('Please log in to your account')
      return;
    }

    if (user.userId && user.email && products) {
      const cartData: CartData = {
        shoppingCartItems: products.map((product) => ({
          productVariantId: product.productVariantId,
          name: product.name,
          price: product.price,
          imageUrl: product.imageUrl,
          quantity: product.quantity,
        })),
        customer: {
          userId: user.userId,
          email: user.email,
        },
      };
      handleSubmitCart(cartData);
    }
  };

  const clearShoppingCart = () => {
    setProducts([]);
  }

  useEffect(() => {
    updateLocalStorage();
  }, [products]);

  const value = useMemo(() => {
    return {
      products,
      addProductToCart,
      initializeShoppingCart,
      deleteProductToCart,
      increaseQuantityProduct,
      decreaseQuantityProduct,
      changeQuantity,
      handleStripe,
      clearShoppingCart
    };
  }, [products]);

  return (
    <ShoppingCartContext.Provider value={value}>
      {children}
    </ShoppingCartContext.Provider>
  );
};

export const useShoppingCart = () => {
  const context = useContext(ShoppingCartContext);

  if (!context) {
    throw new Error(
      "useShoppingCart must be used within a ShoppingCartProvider"
    );
  }

  return context;
};
