import { UserType, UserPermissions } from '@/types/auth';

export const DEFAULT_UNAUTHORIZED_REDIRECT = "/login";
export const DEFAULT_FORBIDDEN_REDIRECT = "/";

export const ROUTE_PERMISSIONS: Record<UserType, UserPermissions> = {

    [UserType.GUEST]: {
        paths: ["/login", "/signup", "/", "/products/*"],
        redirectTo: '/login'
    },
    [UserType.CLIENT]: {
        paths: ["/", "/products/*", "/profile", "/payment/*", "/orders", "/favorites", "/history","/create-store"],
        redirectTo: DEFAULT_FORBIDDEN_REDIRECT
    },
    [UserType.SELLER]: {
        paths: ["/", "/products", "/profile", "/admin/products", "/admin/orders","/store/inventory/*","/store", "/payment/*",],
        redirectTo: "/store/inventory"
    },
    [UserType.OWNER]: {
        paths: ["/", "/products", "/profile","/store/*", "/payment/*",],
        redirectTo: "/store"
    },
    [UserType.ADMIN]: {
        paths: ["*"],
        redirectTo: "/store"
    }
};
