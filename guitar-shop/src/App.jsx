// npm run dev - чтобы запускать


import { useState, useEffect } from "react";

// ================================================================
// 🔧 API — замени BASE_URL на адрес своего ASP.NET Web API
// ================================================================
const BASE_URL = "https://localhost:7027/api";

const api = {
    getGuitars: (params = {}) => {
        const query = new URLSearchParams();
        if (params.brandId) query.append("brandId", params.brandId);
        if (params.categoryId) query.append("categoryId", params.categoryId);
        if (params.search) query.append("search", params.search);
        const qs = query.toString();
        return fetch(`${BASE_URL}/Guitar/guitars${qs ? `?${qs}` : ""}`).then(r => r.json());
    },
    getGuitar: (id) => fetch(`${BASE_URL}/Guitar/guitars/${id}`).then(r => r.json()),
    getBrands: () => fetch(`${BASE_URL}/Brand/brands`).then(r => r.json()),
    getCategories: () => fetch(`${BASE_URL}/Category/categories`).then(r => r.json()),
};



// ================================================================
// УТИЛИТЫ
// ================================================================
function formatPrice(n) {
    return n.toLocaleString("ru-RU") + " $";
}

// ================================================================
// NAVBAR
// ================================================================
function Navbar({ page, setPage, cartCount }) {
    return (
        <nav style={s.nav}>
            <div style={s.navInner}>
                <div style={s.logo} onClick={() => setPage("catalog")}>
                    <span style={s.logoIcon}>♩</span>
                    <span style={s.logoText}>CHORD</span>
                </div>
                <div style={s.navLinks}>
                    <span style={{ ...s.navLink, ...(page === "catalog" ? s.navLinkOn : {}) }} onClick={() => setPage("catalog")}>Каталог</span>
                </div>
                <div style={s.cartBtn} onClick={() => setPage("cart")}>
                    <svg width="20" height="20" fill="none" stroke="currentColor" strokeWidth="1.5" viewBox="0 0 24 24">
                        <path d="M6 2L3 6v14a2 2 0 002 2h14a2 2 0 002-2V6l-3-4z" /><line x1="3" y1="6" x2="21" y2="6" /><path d="M16 10a4 4 0 01-8 0" />
                    </svg>
                    {cartCount > 0 && <span style={s.badge}>{cartCount}</span>}
                </div>
            </div>
        </nav>
    );
}

// ================================================================
// SIDEBAR — фильтры
// ================================================================
function Sidebar({ brands, categories, filters, setFilters }) {
    const toggle = (key, id) => {
        setFilters(f => ({ ...f, [key]: f[key] === id ? null : id }));
    };

    return (
        <aside style={s.sidebar}>
            <div style={s.filterGroup}>
                <p style={s.filterTitle}>Категория</p>
                {categories.map(c => (
                    <label key={c.id} style={s.filterRow}>
                        <span
                            style={{ ...s.filterDot, ...(filters.categoryId === c.id ? s.filterDotOn : {}) }}
                            onClick={() => toggle("categoryId", c.id)}
                        />
                        <span style={{ ...s.filterLabel, ...(filters.categoryId === c.id ? s.filterLabelOn : {}) }}
                            onClick={() => toggle("categoryId", c.id)}>
                            {c.name}
                        </span>
                    </label>
                ))}
            </div>

            <div style={s.filterDivider} />

            <div style={s.filterGroup}>
                <p style={s.filterTitle}>Бренд</p>
                {brands.map(b => (
                    <label key={b.id} style={s.filterRow}>
                        <span
                            style={{ ...s.filterDot, ...(filters.brandId === b.id ? s.filterDotOn : {}) }}
                            onClick={() => toggle("brandId", b.id)}
                        />
                        <span style={{ ...s.filterLabel, ...(filters.brandId === b.id ? s.filterLabelOn : {}) }}
                            onClick={() => toggle("brandId", b.id)}>
                            {b.name}
                        </span>
                    </label>
                ))}
            </div>

            {(filters.brandId || filters.categoryId) && (
                <>
                    <div style={s.filterDivider} />
                    <span style={s.clearBtn} onClick={() => setFilters({ brandId: null, categoryId: null, search: "" })}>
                        Сбросить фильтры
                    </span>
                </>
            )}
        </aside>
    );
}

// ================================================================
// КАРТОЧКА ГИТАРЫ
// ================================================================
function GuitarCard({ guitar, onAdd, onClick }) {
    const [added, setAdded] = useState(false);

    const handleAdd = (e) => {
        e.stopPropagation();
        onAdd(guitar);
        setAdded(true);
        setTimeout(() => setAdded(false), 1400);
    };

    return (
        <div style={s.card} onClick={() => onClick(guitar)}>
            <div style={s.cardImgWrap}>
                <img src={guitar.image} alt={guitar.modelName} style={s.cardImg} />
                <span style={s.cardCat}>{guitar.category?.name}</span>
            </div>
            <div style={s.cardBody}>
                <p style={s.cardBrand}>{guitar.brand?.name}</p>
                <h3 style={s.cardName}>{guitar.modelName}</h3>
                <div style={s.cardFooter}>
                    <span style={s.cardPrice}>{formatPrice(guitar.price)}</span>
                    <button style={{ ...s.addBtn, ...(added ? s.addBtnOn : {}) }} onClick={handleAdd}>
                        {added ? "✓" : "В корзину"}
                    </button>
                </div>
            </div>
        </div>
    );
}

// ================================================================
// КАТАЛОГ
// ================================================================
function CatalogPage({ onAdd, onOpenGuitar }) {
    const [guitars, setGuitars] = useState([]);
    const [brands, setBrands] = useState([]);
    const [categories, setCategories] = useState([]);
    const [filters, setFilters] = useState({ brandId: null, categoryId: null, search: "" });
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        Promise.all([api.getBrands(), api.getCategories()])
            .then(([b, c]) => { setBrands(b); setCategories(c); });
    }, []);

    useEffect(() => {
        setLoading(true);
        api.getGuitars({
            brandId: filters.brandId ?? undefined,
            categoryId: filters.categoryId ?? undefined,
            search: filters.search || undefined,
        })
            .then(setGuitars)
            .finally(() => setLoading(false));
    }, [filters]);

    // 👇 Локальная фильтрация мок-данных — удали после подключения API
    const visible = guitars.filter(g => {
        if (filters.brandId && g.brand?.id !== filters.brandId) return false;
        if (filters.categoryId && g.category?.id !== filters.categoryId) return false;
        if (filters.search && !g.name.toLowerCase().includes(filters.search.toLowerCase())) return false;
        return true;
    });

    return (
        <div style={s.page}>
            {/* Шапка каталога */}
            <div style={s.catalogHeader}>
                <div>
                    <h1 style={s.pageTitle}>Каталог гитар</h1>
                    <p style={s.pageCount}>{visible.length} {declGuitars(visible.length)}</p>
                </div>
                <input
                    style={s.search}
                    placeholder="Поиск по названию..."
                    value={filters.search}
                    onChange={e => setFilters(f => ({ ...f, search: e.target.value }))}
                />
            </div>

            <div style={s.catalogBody}>
                <Sidebar brands={brands} categories={categories} filters={filters} setFilters={setFilters} />

                <div style={{ flex: 1 }}>
                    {loading ? (
                        <div style={s.loading}>Загрузка...</div>
                    ) : visible.length === 0 ? (
                        <div style={s.empty}>
                            <p style={s.emptyIcon}>♪</p>
                            <p style={s.emptyText}>Ничего не найдено</p>
                            <span style={s.clearBtn} onClick={() => setFilters({ brandId: null, categoryId: null, search: "" })}>
                                Сбросить
                            </span>
                        </div>
                    ) : (
                        <div style={s.grid}>
                            {visible.map(g => (
                                <GuitarCard key={g.id} guitar={g} onAdd={onAdd} onClick={onOpenGuitar} />
                            ))}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}

// ================================================================
// СТРАНИЦА ГИТАРЫ
// ================================================================
function GuitarPage({ guitar, onAdd, onBack }) {
    const [added, setAdded] = useState(false);

    const handleAdd = () => {
        onAdd(guitar);
        setAdded(true);
        setTimeout(() => setAdded(false), 1400);
    };

    return (
        <div style={s.page}>
            <div style={s.backRow}>
                <span style={s.back} onClick={onBack}>← Назад в каталог</span>
            </div>
            <div style={s.guitarPage}>
                <div style={s.guitarImgWrap}>
                    <img src={guitar.image} alt={guitar.modelName} style={s.guitarImg} />
                </div>
                <div style={s.guitarInfo}>
                    <p style={s.guitarBrand}>{guitar.brand?.name}</p>
                    <h1 style={s.guitarName}>{guitar.modelName}</h1>
                    <div style={s.guitarTags}>
                        <span style={s.tag}>{guitar.category?.name}</span>
                    </div>
                    {guitar.description && <p style={s.guitarDesc}>{guitar.description}</p>}
                    <p style={s.guitarPrice}>{formatPrice(guitar.price)}</p>
                    <button style={{ ...s.buyBtn, ...(added ? s.buyBtnOn : {}) }} onClick={handleAdd}>
                        {added ? "✓ Добавлено" : "Добавить в корзину"}
                    </button>
                </div>
            </div>
        </div>
    );
}

// ================================================================
// КОРЗИНА
// ================================================================
function CartPage({ cart, onRemove }) {
    const total = cart.reduce((s, i) => s + i.price * i.qty, 0);

    if (cart.length === 0) return (
        <div style={{ ...s.page, textAlign: "center", paddingTop: 100 }}>
            <p style={{ fontSize: 56, marginBottom: 12 }}>♪</p>
            <p style={{ color: "#888", fontSize: 16, letterSpacing: 1 }}>Корзина пуста</p>
        </div>
    );

    return (
        <div style={s.page}>
            <h1 style={s.pageTitle}>Корзина</h1>
            <div style={s.cartList}>
                {cart.map(item => (
                    <div key={item.id} style={s.cartRow}>
                        <img src={item.image} alt={item.name} style={s.cartImg} />
                        <div style={{ flex: 1 }}>
                            <p style={s.cartItemBrand}>{item.brand?.name}</p>
                            <p style={s.cartItemName}>{item.modelName}</p>
                            <p style={s.cartItemQty}>{item.qty} шт.</p>
                        </div>
                        <span style={s.cartItemPrice}>{formatPrice(item.price * item.qty)}</span>
                        <button style={s.removeBtn} onClick={() => onRemove(item.id)}>×</button>
                    </div>
                ))}
            </div>
            <div style={s.cartTotal}>
                <span style={{ color: "#888" }}>Итого</span>
                <span style={s.totalSum}>{formatPrice(total)}</span>
            </div>
            <button style={{ ...s.buyBtn, maxWidth: 320, marginTop: 24 }}>Оформить заказ</button>
        </div>
    );
}

// ================================================================
// СКЛОНЕНИЕ
// ================================================================
function declGuitars(n) {
    if (n % 100 >= 11 && n % 100 <= 14) return "гитар";
    if (n % 10 === 1) return "гитара";
    if (n % 10 >= 2 && n % 10 <= 4) return "гитары";
    return "гитар";
}

// ================================================================
// APP
// ================================================================
export default function App() {
    const [page, setPage] = useState("catalog");
    const [selectedGuitar, setSelectedGuitar] = useState(null);
    const [cart, setCart] = useState([]);

    const addToCart = (guitar) => {
        setCart(prev => {
            const ex = prev.find(i => i.id === guitar.id);
            return ex
                ? prev.map(i => i.id === guitar.id ? { ...i, qty: i.qty + 1 } : i)
                : [...prev, { ...guitar, qty: 1 }];
        });
    };
    const removeFromCart = (id) => setCart(prev => prev.filter(i => i.id !== id));
    const cartCount = cart.reduce((s, i) => s + i.qty, 0);

    const openGuitar = (guitar) => { setSelectedGuitar(guitar); setPage("guitar"); };
    const backToCatalog = () => { setSelectedGuitar(null); setPage("catalog"); };

    return (
        <div style={s.app}>
            <link href="https://fonts.googleapis.com/css2?family=Cormorant+Garamond:ital,wght@0,300;0,400;0,600;1,300&family=DM+Sans:wght@300;400;500&display=swap" rel="stylesheet" />
            <Navbar page={page} setPage={setPage} cartCount={cartCount} />
            <main style={s.main}>
                {page === "catalog" && <CatalogPage onAdd={addToCart} onOpenGuitar={openGuitar} />}
                {page === "guitar" && selectedGuitar && <GuitarPage guitar={selectedGuitar} onAdd={addToCart} onBack={backToCatalog} />}
                {page === "cart" && <CartPage cart={cart} onRemove={removeFromCart} />}
            </main>
            <footer style={s.footer}>CHORD — Магазин гитар © 2026</footer>
        </div>
    );
}

// ================================================================
// СТИЛИ
// ================================================================
const s = {
    app: { fontFamily: "'DM Sans', sans-serif", background: "#f8f7f5", color: "#1c1c1c", minHeight: "100vh" },
    main: { maxWidth: 1280, margin: "0 auto", padding: "0 32px" },

    // Nav
    nav: { background: "#1c1c1c", color: "#f8f7f5", position: "sticky", top: 0, zIndex: 100 },
    navInner: { maxWidth: 1280, margin: "0 auto", padding: "0 32px", height: 60, display: "flex", alignItems: "center", justifyContent: "space-between" },
    logo: { display: "flex", alignItems: "center", gap: 10, cursor: "pointer" },
    logoIcon: { fontSize: 22, color: "#c9a84c" },
    logoText: { fontFamily: "'Cormorant Garamond', serif", fontSize: 20, fontWeight: 600, letterSpacing: 6 },
    navLinks: { display: "flex", gap: 32 },
    navLink: { fontSize: 12, letterSpacing: 2, textTransform: "uppercase", cursor: "pointer", color: "#888", paddingBottom: 2 },
    navLinkOn: { color: "#f8f7f5", borderBottom: "1px solid #c9a84c" },
    cartBtn: { position: "relative", cursor: "pointer", color: "#f8f7f5", display: "flex", alignItems: "center" },
    badge: { position: "absolute", top: -6, right: -8, background: "#c9a84c", color: "#1c1c1c", borderRadius: "50%", width: 17, height: 17, fontSize: 10, fontWeight: 700, display: "flex", alignItems: "center", justifyContent: "center" },

    // Page layout
    page: { padding: "40px 0 80px" },
    catalogHeader: { display: "flex", justifyContent: "space-between", alignItems: "flex-end", marginBottom: 40 },
    pageTitle: { fontFamily: "'Cormorant Garamond', serif", fontSize: 42, fontWeight: 300, margin: "0 0 6px" },
    pageCount: { fontSize: 13, color: "#888", margin: 0 },
    catalogBody: { display: "flex", gap: 48 },

    // Search
    search: { padding: "10px 18px", border: "1px solid #ddd", background: "#fff", fontSize: 14, width: 260, outline: "none", fontFamily: "inherit" },

    // Sidebar
    sidebar: { width: 200, flexShrink: 0 },
    filterGroup: { marginBottom: 8 },
    filterTitle: { fontSize: 11, letterSpacing: 2, textTransform: "uppercase", color: "#aaa", margin: "0 0 14px" },
    filterRow: { display: "flex", alignItems: "center", gap: 10, marginBottom: 10, cursor: "pointer" },
    filterDot: { width: 12, height: 12, border: "1px solid #ccc", flexShrink: 0, cursor: "pointer", transition: "all .15s" },
    filterDotOn: { background: "#1c1c1c", border: "1px solid #1c1c1c" },
    filterLabel: { fontSize: 14, color: "#666", cursor: "pointer" },
    filterLabelOn: { color: "#1c1c1c", fontWeight: 500 },
    filterDivider: { height: 1, background: "#e8e8e4", margin: "20px 0" },
    clearBtn: { fontSize: 12, color: "#c9a84c", cursor: "pointer", letterSpacing: 0.5, textDecoration: "underline" },

    // Grid
    grid: { display: "grid", gridTemplateColumns: "repeat(3, 1fr)", gap: 28 },

    // Card
    card: { background: "#fff", cursor: "pointer", transition: "box-shadow .2s" },
    cardImgWrap: { position: "relative", overflow: "hidden" },
    cardImg: { width: "100%", height: 260, objectFit: "cover", display: "block", transition: "transform .4s" },
    cardCat: { position: "absolute", bottom: 12, left: 12, fontSize: 11, letterSpacing: 1.5, textTransform: "uppercase", background: "rgba(28,28,28,.85)", color: "#f8f7f5", padding: "4px 10px" },
    cardBody: { padding: "18px 18px 20px" },
    cardBrand: { fontSize: 11, letterSpacing: 2, textTransform: "uppercase", color: "#c9a84c", margin: "0 0 5px" },
    cardName: { fontFamily: "'Cormorant Garamond', serif", fontSize: 20, fontWeight: 400, margin: "0 0 14px" },
    cardFooter: { display: "flex", justifyContent: "space-between", alignItems: "center" },
    cardPrice: { fontSize: 16, fontWeight: 500 },

    addBtn: { padding: "8px 16px", background: "transparent", border: "1px solid #1c1c1c", cursor: "pointer", fontSize: 12, letterSpacing: 1, textTransform: "uppercase", transition: "all .2s", fontFamily: "inherit", color: "#1c1c1c" },
    addBtnOn: { background: "#1c1c1c", color: "#fff" },

    // Loading / empty
    loading: { textAlign: "center", padding: 80, color: "#888", fontSize: 14, letterSpacing: 1 },
    empty: { textAlign: "center", padding: "80px 0" },
    emptyIcon: { fontSize: 48, margin: "0 0 12px", color: "#ccc" },
    emptyText: { color: "#888", fontSize: 15, marginBottom: 16 },

    // Guitar page
    backRow: { marginBottom: 32 },
    back: { fontSize: 13, color: "#888", cursor: "pointer", letterSpacing: 0.5 },
    guitarPage: { display: "grid", gridTemplateColumns: "1fr 1fr", gap: 64, alignItems: "start" },
    guitarImgWrap: { position: "sticky", top: 80 },
    guitarImg: { width: "100%", aspectRatio: "4/3", objectFit: "cover", display: "block" },
    guitarInfo: { paddingTop: 8 },
    guitarBrand: { fontSize: 12, letterSpacing: 3, textTransform: "uppercase", color: "#c9a84c", margin: "0 0 12px" },
    guitarName: { color: "rgb(28, 28, 28)", fontFamily: "'Cormorant Garamond', serif", fontSize: 52, fontWeight: 300, margin: "0 0 20px", lineHeight: 1.1 },
    guitarTags: { display: "flex", gap: 10, marginBottom: 28 },
    tag: { fontSize: 11, letterSpacing: 1.5, textTransform: "uppercase", background: "#f0ede8", padding: "5px 14px", color: "#666" },
    guitarDesc: { fontSize: 15, lineHeight: 1.7, color: "#555", margin: "0 0 36px" },
    guitarPrice: { fontFamily: "'Cormorant Garamond', serif", fontSize: 40, fontWeight: 400, margin: "0 0 28px" },
    buyBtn: { padding: "14px 40px", background: "#1c1c1c", color: "#f8f7f5", border: "none", cursor: "pointer", fontSize: 13, letterSpacing: 2, textTransform: "uppercase", fontFamily: "inherit", display: "block", transition: "background .2s" },
    buyBtnOn: { background: "#3a7a3a" },

    // Cart
    cartList: { borderTop: "1px solid #e8e8e4", marginBottom: 0 },
    cartRow: { display: "flex", alignItems: "center", gap: 20, padding: "20px 0", borderBottom: "1px solid #e8e8e4" },
    cartImg: { width: 90, height: 90, objectFit: "cover", flexShrink: 0 },
    cartItemBrand: { fontSize: 11, letterSpacing: 2, textTransform: "uppercase", color: "#c9a84c", margin: "0 0 4px" },
    cartItemName: { fontFamily: "'Cormorant Garamond', serif", fontSize: 18, margin: "0 0 4px" },
    cartItemQty: { fontSize: 13, color: "#888", margin: 0 },
    cartItemPrice: { fontSize: 16, fontWeight: 500, minWidth: 130, textAlign: "right" },
    removeBtn: { background: "none", border: "none", fontSize: 22, cursor: "pointer", color: "#bbb", padding: "0 8px", lineHeight: 1 },
    cartTotal: { display: "flex", justifyContent: "space-between", alignItems: "center", padding: "24px 0", borderTop: "2px solid #1c1c1c", marginTop: 4 },
    totalSum: { fontFamily: "'Cormorant Garamond', serif", fontSize: 32 },

    // Footer
    footer: { borderTop: "1px solid #e8e8e4", padding: "28px 32px", textAlign: "center", fontSize: 11, letterSpacing: 3, textTransform: "uppercase", color: "#aaa", marginTop: 40 },
};