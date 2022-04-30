import './Page.css'

const ForgotPasswordPage = () => {
    return (
        <>
            <div className="content">
                <div className="header-text">Forgot Password</div>
                <form action="#">
                    <div className="field">
                        <span className="fa fa-user"></span>
                        <input type="text" placeholder="Email" required />
                    </div>
                    <button>Send email</button>
                </form>
            </div>
        </>
    )
}

export { ForgotPasswordPage }