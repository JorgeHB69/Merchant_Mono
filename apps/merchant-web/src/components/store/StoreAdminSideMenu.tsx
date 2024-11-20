import React from "react";
import styles from "@/styles/SideMenu.module.css";
import { Option } from "@/components/Option";
import { LuHome } from "react-icons/lu";
import { MdOutlineInventory2 } from "react-icons/md";
import { RiShoppingCart2Line } from "react-icons/ri";
import { TbUsers } from "react-icons/tb";
import "@/styles/store/StoreAdminMenu.css";

interface StoreAdminSideMenuProps {
  onRouteChange: (route: string) => void;
}
export const StoreAdminSideMenu = ({
  onRouteChange,
}: StoreAdminSideMenuProps) => {
  return (
    <div
      className={`${styles.sideMenu} desktop-store-admin-menu`}
      style={{ paddingTop: "25px", height: "100%" }}
    >
      <Option
        icon={LuHome}
        text={"Home"}
        route="stores"
        completeRoute={true}
        onRouteChange={onRouteChange}
      />
      <Option
        icon={MdOutlineInventory2}
        text={"Store"}
        route="stores"
        onRouteChange={onRouteChange}
      />
      <Option
        icon={RiShoppingCart2Line}
        text={"Inventory"}
        route="stores"
        onRouteChange={onRouteChange}
      />
      <Option
        icon={TbUsers}
        text={"Members"}
        route="stores"
        onRouteChange={onRouteChange}
      />
    </div>
  );
};
