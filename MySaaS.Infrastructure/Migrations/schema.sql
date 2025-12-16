--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: common; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA common;


ALTER SCHEMA common OWNER TO postgres;

--
-- Name: inventory; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA inventory;


ALTER SCHEMA inventory OWNER TO postgres;

--
-- Name: production; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA production;


ALTER SCHEMA production OWNER TO postgres;

--
-- Name: products; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA products;


ALTER SCHEMA products OWNER TO postgres;

--
-- Name: purchases; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA purchases;


ALTER SCHEMA purchases OWNER TO postgres;

--
-- Name: sales; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA sales;


ALTER SCHEMA sales OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: items; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.items (
    item_id integer NOT NULL,
    name text NOT NULL,
    description text
);


ALTER TABLE common.items OWNER TO postgres;

--
-- Name: items_item_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

ALTER TABLE common.items ALTER COLUMN item_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME common.items_item_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: unit_conversions; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.unit_conversions (
    id integer NOT NULL,
    id_item integer NOT NULL,
    from_unit_id integer NOT NULL,
    to_unit_id integer NOT NULL,
    factor double precision NOT NULL
);


ALTER TABLE common.unit_conversions OWNER TO postgres;

--
-- Name: unit_conversions_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

ALTER TABLE common.unit_conversions ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME common.unit_conversions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: unities; Type: TABLE; Schema: common; Owner: postgres
--

CREATE TABLE common.unities (
    unit_id integer NOT NULL,
    name text NOT NULL
);


ALTER TABLE common.unities OWNER TO postgres;

--
-- Name: unities_unit_id_seq; Type: SEQUENCE; Schema: common; Owner: postgres
--

ALTER TABLE common.unities ALTER COLUMN unit_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME common.unities_unit_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: stock_movements; Type: TABLE; Schema: inventory; Owner: postgres
--

CREATE TABLE inventory.stock_movements (
    id integer NOT NULL,
    item_id integer NOT NULL,
    movement_type character varying(3) NOT NULL,
    quantity numeric NOT NULL,
    unit_id integer NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL
);


ALTER TABLE inventory.stock_movements OWNER TO postgres;

--
-- Name: stock_movements_id_seq; Type: SEQUENCE; Schema: inventory; Owner: postgres
--

ALTER TABLE inventory.stock_movements ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME inventory.stock_movements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: ingredients; Type: TABLE; Schema: production; Owner: postgres
--

CREATE TABLE production.ingredients (
    id_item integer NOT NULL,
    id_recipe integer
);


ALTER TABLE production.ingredients OWNER TO postgres;

--
-- Name: recipe_ingredients; Type: TABLE; Schema: production; Owner: postgres
--

CREATE TABLE production.recipe_ingredients (
    id_recipe integer NOT NULL,
    id_ingredient integer NOT NULL,
    weight_unit_id integer NOT NULL,
    weight_quantity numeric NOT NULL,
    waste_unit_id integer NOT NULL,
    waste_quantity numeric NOT NULL,
    id integer NOT NULL
);


ALTER TABLE production.recipe_ingredients OWNER TO postgres;

--
-- Name: recipe_ingredients_id_seq; Type: SEQUENCE; Schema: production; Owner: postgres
--

ALTER TABLE production.recipe_ingredients ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME production.recipe_ingredients_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: recipes; Type: TABLE; Schema: production; Owner: postgres
--

CREATE TABLE production.recipes (
    recipe_id integer NOT NULL,
    quantity_unit_id integer NOT NULL,
    quantity numeric NOT NULL
);


ALTER TABLE production.recipes OWNER TO postgres;

--
-- Name: recipes_recipe_id_seq; Type: SEQUENCE; Schema: production; Owner: postgres
--

ALTER TABLE production.recipes ALTER COLUMN recipe_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME production.recipes_recipe_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: product_categories; Type: TABLE; Schema: products; Owner: postgres
--

CREATE TABLE products.product_categories (
    id integer NOT NULL,
    name text NOT NULL
);


ALTER TABLE products.product_categories OWNER TO postgres;

--
-- Name: product_categories_id_seq; Type: SEQUENCE; Schema: products; Owner: postgres
--

ALTER TABLE products.product_categories ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME products.product_categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: product_categories_link; Type: TABLE; Schema: products; Owner: postgres
--

CREATE TABLE products.product_categories_link (
    id integer NOT NULL,
    product_category_id integer NOT NULL,
    product_id integer NOT NULL
);


ALTER TABLE products.product_categories_link OWNER TO postgres;

--
-- Name: product_categories_link_id_seq; Type: SEQUENCE; Schema: products; Owner: postgres
--

ALTER TABLE products.product_categories_link ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME products.product_categories_link_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: product_items; Type: TABLE; Schema: products; Owner: postgres
--

CREATE TABLE products.product_items (
    id_product integer NOT NULL,
    id_item integer NOT NULL,
    weight_unit_id integer NOT NULL,
    weight_quantity numeric NOT NULL,
    waste_unit_id integer NOT NULL,
    waste_quantity numeric NOT NULL
);


ALTER TABLE products.product_items OWNER TO postgres;

--
-- Name: products; Type: TABLE; Schema: products; Owner: postgres
--

CREATE TABLE products.products (
    id_item integer NOT NULL,
    price numeric NOT NULL,
    id_recipe integer
);


ALTER TABLE products.products OWNER TO postgres;

--
-- Name: purchase_items; Type: TABLE; Schema: purchases; Owner: postgres
--

CREATE TABLE purchases.purchase_items (
    id integer NOT NULL,
    purchase_id integer NOT NULL,
    item_id integer NOT NULL,
    unit_id integer NOT NULL,
    quantity numeric NOT NULL,
    cost numeric,
    movement_id integer NOT NULL
);


ALTER TABLE purchases.purchase_items OWNER TO postgres;

--
-- Name: purchase_items_id_seq; Type: SEQUENCE; Schema: purchases; Owner: postgres
--

ALTER TABLE purchases.purchase_items ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME purchases.purchase_items_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: purchases; Type: TABLE; Schema: purchases; Owner: postgres
--

CREATE TABLE purchases.purchases (
    id integer NOT NULL,
    date date NOT NULL
);


ALTER TABLE purchases.purchases OWNER TO postgres;

--
-- Name: purchases_id_seq; Type: SEQUENCE; Schema: purchases; Owner: postgres
--

ALTER TABLE purchases.purchases ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME purchases.purchases_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: categories_requirements; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.categories_requirements (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    category_id integer NOT NULL,
    quantity numeric
);


ALTER TABLE sales.categories_requirements OWNER TO postgres;

--
-- Name: categories_reward; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.categories_reward (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    category_id integer NOT NULL,
    quantity numeric
);


ALTER TABLE sales.categories_reward OWNER TO postgres;

--
-- Name: categories_reward_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.categories_reward ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.categories_reward_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: category_requirements_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.categories_requirements ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.category_requirements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: discounts_rewards; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.discounts_rewards (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    discount_percentage numeric NOT NULL
);


ALTER TABLE sales.discounts_rewards OWNER TO postgres;

--
-- Name: discounts_rewards_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.discounts_rewards ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.discounts_rewards_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: prices_rewards; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.prices_rewards (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    new_price numeric NOT NULL
);


ALTER TABLE sales.prices_rewards OWNER TO postgres;

--
-- Name: prices_rewards_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.prices_rewards ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.prices_rewards_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: products_requirements; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.products_requirements (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    product_id integer NOT NULL,
    quantity numeric NOT NULL
);


ALTER TABLE sales.products_requirements OWNER TO postgres;

--
-- Name: product_requirements_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.products_requirements ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.product_requirements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: products_rewards; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.products_rewards (
    id integer NOT NULL,
    promotion_id integer NOT NULL,
    product_id integer NOT NULL,
    quantity numeric
);


ALTER TABLE sales.products_rewards OWNER TO postgres;

--
-- Name: product_reward_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.products_rewards ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.product_reward_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: promotions; Type: TABLE; Schema: sales; Owner: postgres
--

CREATE TABLE sales.promotions (
    id integer NOT NULL,
    name text NOT NULL,
    repeatable boolean NOT NULL
);


ALTER TABLE sales.promotions OWNER TO postgres;

--
-- Name: promotions_id_seq; Type: SEQUENCE; Schema: sales; Owner: postgres
--

ALTER TABLE sales.promotions ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME sales.promotions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: items items_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.items
    ADD CONSTRAINT items_pkey PRIMARY KEY (item_id);


--
-- Name: unit_conversions unit_conversions_id_item_from_unit_id_to_unit_id_key; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unit_conversions
    ADD CONSTRAINT unit_conversions_id_item_from_unit_id_to_unit_id_key UNIQUE (id_item, from_unit_id, to_unit_id);


--
-- Name: unit_conversions unit_conversions_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unit_conversions
    ADD CONSTRAINT unit_conversions_pkey PRIMARY KEY (id);


--
-- Name: unities unities_pkey; Type: CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unities
    ADD CONSTRAINT unities_pkey PRIMARY KEY (unit_id);


--
-- Name: stock_movements stock_movements_pkey; Type: CONSTRAINT; Schema: inventory; Owner: postgres
--

ALTER TABLE ONLY inventory.stock_movements
    ADD CONSTRAINT stock_movements_pkey PRIMARY KEY (id);


--
-- Name: ingredients ingredients_pkey; Type: CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.ingredients
    ADD CONSTRAINT ingredients_pkey PRIMARY KEY (id_item);


--
-- Name: recipe_ingredients recipe_ingredients_pkey; Type: CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipe_ingredients
    ADD CONSTRAINT recipe_ingredients_pkey PRIMARY KEY (id);


--
-- Name: recipes recipes_pkey; Type: CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipes
    ADD CONSTRAINT recipes_pkey PRIMARY KEY (recipe_id);


--
-- Name: product_categories_link product_categories_link_pkey; Type: CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_categories_link
    ADD CONSTRAINT product_categories_link_pkey PRIMARY KEY (id);


--
-- Name: product_categories product_categories_pkey; Type: CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_categories
    ADD CONSTRAINT product_categories_pkey PRIMARY KEY (id);


--
-- Name: product_items product_items_pkey; Type: CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_items
    ADD CONSTRAINT product_items_pkey PRIMARY KEY (id_product, id_item);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id_item);


--
-- Name: purchase_items purchase_items_pkey; Type: CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchase_items
    ADD CONSTRAINT purchase_items_pkey PRIMARY KEY (id);


--
-- Name: purchases purchases_pkey; Type: CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchases
    ADD CONSTRAINT purchases_pkey PRIMARY KEY (id);


--
-- Name: categories_reward categories_reward_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_reward
    ADD CONSTRAINT categories_reward_pkey PRIMARY KEY (id);


--
-- Name: categories_requirements category_requirements_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_requirements
    ADD CONSTRAINT category_requirements_pkey PRIMARY KEY (id);


--
-- Name: discounts_rewards discounts_rewards_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.discounts_rewards
    ADD CONSTRAINT discounts_rewards_pkey PRIMARY KEY (id);


--
-- Name: prices_rewards prices_rewards_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.prices_rewards
    ADD CONSTRAINT prices_rewards_pkey PRIMARY KEY (id);


--
-- Name: products_requirements product_requirements_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_requirements
    ADD CONSTRAINT product_requirements_pkey PRIMARY KEY (id);


--
-- Name: products_rewards product_reward_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_rewards
    ADD CONSTRAINT product_reward_pkey PRIMARY KEY (id);


--
-- Name: promotions promotions_pkey; Type: CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.promotions
    ADD CONSTRAINT promotions_pkey PRIMARY KEY (id);


--
-- Name: unit_conversions unit_conversions_from_unit_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unit_conversions
    ADD CONSTRAINT unit_conversions_from_unit_id_fkey FOREIGN KEY (from_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: unit_conversions unit_conversions_id_item_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unit_conversions
    ADD CONSTRAINT unit_conversions_id_item_fkey FOREIGN KEY (id_item) REFERENCES common.items(item_id);


--
-- Name: unit_conversions unit_conversions_to_unit_id_fkey; Type: FK CONSTRAINT; Schema: common; Owner: postgres
--

ALTER TABLE ONLY common.unit_conversions
    ADD CONSTRAINT unit_conversions_to_unit_id_fkey FOREIGN KEY (to_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: stock_movements stock_movements_item_id_fkey; Type: FK CONSTRAINT; Schema: inventory; Owner: postgres
--

ALTER TABLE ONLY inventory.stock_movements
    ADD CONSTRAINT stock_movements_item_id_fkey FOREIGN KEY (item_id) REFERENCES common.items(item_id);


--
-- Name: stock_movements stock_movements_unit_id_fkey; Type: FK CONSTRAINT; Schema: inventory; Owner: postgres
--

ALTER TABLE ONLY inventory.stock_movements
    ADD CONSTRAINT stock_movements_unit_id_fkey FOREIGN KEY (unit_id) REFERENCES common.unities(unit_id);


--
-- Name: ingredients fk_ingredient_recipe; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.ingredients
    ADD CONSTRAINT fk_ingredient_recipe FOREIGN KEY (id_recipe) REFERENCES production.recipes(recipe_id);


--
-- Name: CONSTRAINT fk_ingredient_recipe ON ingredients; Type: COMMENT; Schema: production; Owner: postgres
--

COMMENT ON CONSTRAINT fk_ingredient_recipe ON production.ingredients IS 'an ingredient can have a recipe';


--
-- Name: recipe_ingredients fk_recipe_id; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipe_ingredients
    ADD CONSTRAINT fk_recipe_id FOREIGN KEY (id_recipe) REFERENCES production.recipes(recipe_id) ON DELETE CASCADE;


--
-- Name: ingredients ingredients_id_item_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.ingredients
    ADD CONSTRAINT ingredients_id_item_fkey FOREIGN KEY (id_item) REFERENCES common.items(item_id);


--
-- Name: recipe_ingredients recipe_ingredients_id_ingredient_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipe_ingredients
    ADD CONSTRAINT recipe_ingredients_id_ingredient_fkey FOREIGN KEY (id_ingredient) REFERENCES production.ingredients(id_item);


--
-- Name: recipe_ingredients recipe_ingredients_waste_unit_id_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipe_ingredients
    ADD CONSTRAINT recipe_ingredients_waste_unit_id_fkey FOREIGN KEY (waste_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: recipe_ingredients recipe_ingredients_weight_unit_id_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipe_ingredients
    ADD CONSTRAINT recipe_ingredients_weight_unit_id_fkey FOREIGN KEY (weight_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: recipes recipes_quantity_unit_id_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipes
    ADD CONSTRAINT recipes_quantity_unit_id_fkey FOREIGN KEY (quantity_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: recipes recipes_recipe_id_fkey; Type: FK CONSTRAINT; Schema: production; Owner: postgres
--

ALTER TABLE ONLY production.recipes
    ADD CONSTRAINT recipes_recipe_id_fkey FOREIGN KEY (recipe_id) REFERENCES common.items(item_id) NOT VALID;


--
-- Name: products fk_product_recipe; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.products
    ADD CONSTRAINT fk_product_recipe FOREIGN KEY (id_recipe) REFERENCES production.recipes(recipe_id);


--
-- Name: product_categories_link product_categories_link_product_category_id_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_categories_link
    ADD CONSTRAINT product_categories_link_product_category_id_fkey FOREIGN KEY (product_category_id) REFERENCES products.product_categories(id);


--
-- Name: product_categories_link product_categories_link_product_id_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_categories_link
    ADD CONSTRAINT product_categories_link_product_id_fkey FOREIGN KEY (product_id) REFERENCES products.products(id_item);


--
-- Name: product_items product_items_id_item_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_items
    ADD CONSTRAINT product_items_id_item_fkey FOREIGN KEY (id_item) REFERENCES common.items(item_id);


--
-- Name: product_items product_items_id_product_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_items
    ADD CONSTRAINT product_items_id_product_fkey FOREIGN KEY (id_product) REFERENCES products.products(id_item);


--
-- Name: product_items product_items_waste_unit_id_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_items
    ADD CONSTRAINT product_items_waste_unit_id_fkey FOREIGN KEY (waste_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: product_items product_items_weight_unit_id_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.product_items
    ADD CONSTRAINT product_items_weight_unit_id_fkey FOREIGN KEY (weight_unit_id) REFERENCES common.unities(unit_id);


--
-- Name: products products_id_item_fkey; Type: FK CONSTRAINT; Schema: products; Owner: postgres
--

ALTER TABLE ONLY products.products
    ADD CONSTRAINT products_id_item_fkey FOREIGN KEY (id_item) REFERENCES common.items(item_id);


--
-- Name: purchase_items purchase_items_item_id_fkey; Type: FK CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchase_items
    ADD CONSTRAINT purchase_items_item_id_fkey FOREIGN KEY (item_id) REFERENCES common.items(item_id);


--
-- Name: purchase_items purchase_items_movement_id_fkey; Type: FK CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchase_items
    ADD CONSTRAINT purchase_items_movement_id_fkey FOREIGN KEY (movement_id) REFERENCES inventory.stock_movements(id);


--
-- Name: purchase_items purchase_items_purchase_id_fkey; Type: FK CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchase_items
    ADD CONSTRAINT purchase_items_purchase_id_fkey FOREIGN KEY (purchase_id) REFERENCES purchases.purchases(id);


--
-- Name: purchase_items purchase_items_unit_id_fkey; Type: FK CONSTRAINT; Schema: purchases; Owner: postgres
--

ALTER TABLE ONLY purchases.purchase_items
    ADD CONSTRAINT purchase_items_unit_id_fkey FOREIGN KEY (unit_id) REFERENCES common.unities(unit_id);


--
-- Name: categories_reward categories_reward_category_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_reward
    ADD CONSTRAINT categories_reward_category_id_fkey FOREIGN KEY (category_id) REFERENCES products.product_categories(id);


--
-- Name: categories_reward categories_reward_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_reward
    ADD CONSTRAINT categories_reward_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- Name: categories_requirements category_requirements_category_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_requirements
    ADD CONSTRAINT category_requirements_category_id_fkey FOREIGN KEY (category_id) REFERENCES products.product_categories(id);


--
-- Name: categories_requirements category_requirements_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.categories_requirements
    ADD CONSTRAINT category_requirements_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- Name: discounts_rewards discounts_rewards_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.discounts_rewards
    ADD CONSTRAINT discounts_rewards_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- Name: prices_rewards prices_rewards_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.prices_rewards
    ADD CONSTRAINT prices_rewards_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- Name: products_requirements product_requirements_product_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_requirements
    ADD CONSTRAINT product_requirements_product_id_fkey FOREIGN KEY (product_id) REFERENCES products.products(id_item);


--
-- Name: products_requirements product_requirements_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_requirements
    ADD CONSTRAINT product_requirements_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- Name: products_rewards product_reward_product_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_rewards
    ADD CONSTRAINT product_reward_product_id_fkey FOREIGN KEY (product_id) REFERENCES products.products(id_item);


--
-- Name: products_rewards product_reward_promotion_id_fkey; Type: FK CONSTRAINT; Schema: sales; Owner: postgres
--

ALTER TABLE ONLY sales.products_rewards
    ADD CONSTRAINT product_reward_promotion_id_fkey FOREIGN KEY (promotion_id) REFERENCES sales.promotions(id);


--
-- PostgreSQL database dump complete
--

